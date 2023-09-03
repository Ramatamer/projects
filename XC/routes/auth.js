const router = require("express").Router();
const conn = require("../database/connection")
const { body, validationResult } = require('express-validator');
const util = require("util");
const bcrypt = require("bcrypt");
const crypto = require("crypto");
const { query } = require("express");


//=========== REGISTER =============//
router.post(
  "/register",
  body("email").isEmail().withMessage("please enter a valid email!"),
  body("name")
    .isString()
    .withMessage("please enter a valid name")
    .isLength({ min: 10, max: 20 })
    .withMessage("name should be between (10-20) character"),
  body("password")
    .isLength({ min: 8, max: 12 })
    .withMessage("password should be between (8-12) character"),
  body("phone")
    .isLength({ min: 8, max: 12 })
    .withMessage("phone should be between (8-12) character"),
  async (req, res) => {
    try {
      // 1- VALIDATION REQUEST [manual, express validation]
      const errors = validationResult(req);
      if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() });
      }

      // 2- CHECK IF EMAIL EXISTS
      const query = util.promisify(conn.query).bind(conn); // transform query mysql --> promise to use [await/async]
      const checkEmailExists = await query(
        "select * from user where email = ?",
        [req.body.email]
      );
      if (checkEmailExists.length > 0) {
        res.status(400).json(errors);
      }

      // 3- PREPARE OBJECT USER TO -> SAVE
      const userData = {
        name: req.body.name,
        email: req.body.email,
        phone: req.body.phone,
        password: await bcrypt.hash(req.body.password, 10),
        token: crypto.randomBytes(16).toString("hex"), // JSON WEB TOKEN, CRYPTO -> RANDOM ENCRYPTION STANDARD
      };

      // 4- INSERT USER OBJECT INTO DB
      await query("insert into user set ? ", userData);
      delete userData.password;
      res.status(200).json(userData);
    } catch (err) {
      res.status(500).json({ err: err });
    }
  }
);


//============ LOGIN FUNCTION ==============//
router.post(
  "/login",
  body("email").isEmail().withMessage("please enter a valid email!"),
  body("password")
    .isLength({ min: 8, max: 12 })
    .withMessage("password should be between (8-12) character"),
  async (req, res) => {
    try {
      // 1- VALIDATION REQUEST [manual, express validation]
      const errors = validationResult(req);
      if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() });
      }

      // 2- CHECK IF EMAIL EXISTS
      const query = util.promisify(conn.query).bind(conn); // transform query mysql --> promise to use [await/async]
      const user = await query("select * from user where email = ?", [req.body.email]);
      if (user.length == 0) {
        res.status(404).json({
          errors: [
            {
              msg: "email or password not found !",
            },
          ],
        });
      }
      // 3- COMPARE HASHED PASSWORD
      const checkPassword = await bcrypt.compare(
        req.body.password,
        user[0].password
      );
      if (checkPassword) {
        delete user[0].password;
        res.status(200).json(user[0]);
      } else {
        res.status(404).json({
          errors: [
            {
              msg: "email or password not found !",
            },
          ],
        });
      }
    } catch (err) {
      res.status(500).json({ err: err });
    }
  }
);

//====== ADMIN GET ALL USER  ======//
router.get("/getalluser", async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({
      msg: "error"
    })
  }
  const query = util.promisify(conn.query).bind(conn);
  const userobg = await query("select * from user");
  res.status(200).json(userobg);
})

//====== ADMIN DELETE USER  ======//
router.delete('/deleteuser/:id', async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({
      msg: 'Error: Invalid request parameters',
    });
  }

  const id = req.params.id;

  try {
    const query = util.promisify(conn.query).bind(conn);
    const result = await query('DELETE FROM user WHERE id = ?', [id]);
    if (result.affectedRows === 0) {
      return res.status(404).json({
        msg: 'Error: User not found',
      });
    }
    return res.status(200).json({
      msg: 'User deleted successfully',
    });
  } catch (err) {
    console.log(err);
    return res.status(500).json({
      msg: 'Error: Internal server error',
    });
  }
});

//====== ADMIN EDIT USER  ======//
router.put("/editusers/:id", async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({ errors: errors.array() });
  }

  const { name, email, statues, phone, role } = req.body;
  const userId = req.params.id;

  try {
    const query = util.promisify(conn.query).bind(conn);
    const updateUser = await query(
      "UPDATE user SET name = ?, email = ?, statues = ?, role = ?, phone = ? WHERE id = ?",
      [name, email, statues, role, phone, userId]
    );

    if (updateUser.affectedRows === 0) {
      return res.status(404).json({ msg: "User not found" });
    }

    res.json({ msg: "User updated successfully" });
  } catch (error) {
    console.error(error);
    res.status(500).send("Server Error");
  }
});

//====== ADMIN ACTIVATE ======//

router.put("/activate/:id", async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({ errors: errors.array() });
  }

  const { statues } = req.body;
  const userId = req.params.id;

  try {
    const query = util.promisify(conn.query).bind(conn);
    const updateUser = await query(
      "UPDATE user SET statues = ? WHERE id = ?",
      [statues, userId]
    );

    if (updateUser.affectedRows === 0) {
      return res.status(404).json({ msg: "User not found" });
    }

    res.json({ msg: "User status updated successfully" });
  } catch (error) {
    console.error(error);
    res.status(500).send("Server Error");
  }
});

//====== ADMIN GET ALL USER NOT ACTIVE ======//
router.get("/getnonactiveuser", async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({
      msg: "error"
    })
  }
  const query = util.promisify(conn.query).bind(conn);
  const userobg = await query("select * from user where statues = 0");
  res.status(200).json(userobg);
})
//====== ADMIN APPROVE OF REQUEST USER ======//
router.put("/approve", body("id"), async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({
      msg: "error"
    })
  }
  const userid = req.body.id;
  const userData = {
    statues: 1
  };
  const query = util.promisify(conn.query).bind(conn);
  await query("update user set ? where id =?", [userData, userid])
  res.status(200).json({
    msg: "THE USER REQUEST APPROVED"
  })
})


module.exports = router;
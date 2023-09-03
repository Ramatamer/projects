const router = require("express").Router();
const conn = require("../database/connection")
const authorized = require("../middleware/authorized");
const admin = require("../middleware/admin");
const { body, validationResult } = require('express-validator');
const upload = require("../middleware/audio");
const { query } = require("express");
const util = require("util");
const fs = require("fs");



// ADMIN APPROVE THE REQUEST LOGIN OF USER
router.put("/approve/:id", admin, body("statues"), async (req, res) => {

    const query = util.promisify(conn.query).bind(conn);
    const userid = req.params.id;
    const user = await query("select * from user where id = ?", userid)
    if (!user[0]) {
        res.status(404).json({
            msg: "the user not found "
        })
    }
    const userdata = {
        statues: req.body.statues
    }
    await query("update user set ? where id = ?", [userdata, userid])
    res.json(userdata)

})

// get all users
router.get("/getallUsers", admin, async (req, res) => {
    const query = util.promisify(conn.query).bind(conn);
    const users = await query("select * from user where statues = 0 ")
    res.status(200).json(users);
})

router.get("/getuser/:id", async (req, res) => {
    const query = util.promisify(conn.query).bind(conn);
    const user = await query("select * from user where id = ?", [
        req.params.id,
    ]);
    if (!user[0]) {
        res.status(404).json({ ms: "user not found !" });
    }
    res.status(200).json(user[0]);
});

// USER SOLVE QUESTIONS 
router.post("/solve/:id", authorized, body("sq1"), body("sq2"), body("sq3"), body("sq4"), body("sq5"), body("sq6"), body("sq7"), body("sq8"), body("sq9"), body("sq10"), async (req, res) => {
    const examid = req.params.id;
    const query = util.promisify(conn.query).bind(conn);
    const exam = query("select * from exam where examID = ?", examid);
    if (!exam[0]) {
        res.status(404).json({
            msg: "the exam not found"
        })
    }
    const solve = {

    }
})
module.exports = router;
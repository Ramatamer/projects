const router = require("express").Router();
const conn = require("../database/connection")
const authorized = require("../middleware/authorized");
const admin = require("../middleware/admin");
const { body, validationResult } = require('express-validator');
const upload = require("../middleware/audio");
const { query } = require("express");
const util = require("util");
const fs = require("fs");

//==== ADMIN CREATE / UPDATE / DELETE =======//
// === CRAEATE EXAM 
router.post("/create", upload.single("audio"),
  body("questionText").isString().withMessage("please enter the valid text"), async (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return res.status(400).json({
        msg: "error"
      })
    }
    if (!req.file) {
      res.status(404).json({
        msg: " the audio file required"
      })
    }
    // prepare exam object
    const exam = {
      questionText: req.body.questionText,
      audioFile: req.file.originalname,
    }
    // insert object to data base 
    const query = util.promisify(conn.query).bind(conn);
    await query("insert into exam set ?", exam);

    res.status(200).json(req.body)
  })



//========GET ALL EXAM ====//
router.get("/getallexam", async (req, res) => {
  const query = util.promisify(conn.query).bind(conn);
  const exam = await query("select * from exam");
  res.json(exam);
});



//===========USER==========//

// user can get exam
router.get("/getexam/:id", async (req, res) => {
  const examid = req.params.id;
  const query = util.promisify(conn.query).bind(conn);
  const exam = await query("select * from exam where examID = ?", examid)
  if (!exam[0]) {
    res.status(404).json({
      msg: "the exam not found "
    })
  }
  else {
    res.json(exam[0]);
  }

})

router.delete('/deleteexam/:id', async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({
      msg: 'Error: Invalid request parameters',
    });
  }

  const id = req.params.id;

  try {
    const query = util.promisify(conn.query).bind(conn);
    const result = await query('DELETE FROM exam WHERE examID = ?', [id]);
    if (result.affectedRows === 0) {
      return res.status(404).json({
        msg: 'Error: Exam not found',
      });
    }
    return res.status(200).json({
      msg: 'Exam deleted successfully',
    });
  } catch (err) {
    console.log(err);
    return res.status(500).json({
      msg: 'Error: Internal server error',
    });
  }
});

// // === UPDATE EXAM 
// router.put("/:id",upload.single("audio"),
//     body("questionText"),async (req,res)=>{
//         const errors = validationResult(req);
//             if (!errors.isEmpty()) {
//                 return res.status(400).json({
//                     msg:"error"
//                 })
//             }
//             const query = util.promisify(conn.query).bind(conn);  
//             // check id exam exist or not 
//             const exam = await query("select * from exam where examID = ?",[req.params.id,])
//             if (!exam[0])
//             {
//                 res.status(404).json({
//                     msg:"the exam not found "
//                 })
//             }
//             // prepare exam object
//             const examobj = {
//                 questionText : req.body.questionText,
//             }
//             if (req.file)
//             {
//                 examobj.audioFile = req.file.originalname;
//                 // delete old audio
//                 fs.unlinkSync("./upload/"+exam[0].audioFile);

//             }
//             // update object to data base 
//             await query ("update exam set ? where examID = ?", [examobj , exam[0].id]) ;

//             res.status(200).json(req.body)
// })

module.exports = router;
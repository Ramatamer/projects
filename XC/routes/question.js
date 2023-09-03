const router = require("express").Router();
const conn = require("../database/connection")
const authorized = require("../middleware/authorized");
const admin = require("../middleware/admin");
const { body, validationResult } = require('express-validator');
const { query } = require("express");
const util = require("util");

//=== ADMIN CREATE UPDATE DELETE QUESTION
// CRAATE 
router.post("/create", admin,
    body("questionText").isString().withMessage("PLEASE ENTER THE VALID QUESTION"),
    body("choice1").isString().withMessage("please enter valid choice"),
    body("choice2").isString().withMessage("please enter valid choice"),
    body("choice3").isString().withMessage("please enter valid choice"),
    body("choice4").isString().withMessage("please enter valid choice"),
    body("rhightChoice").isString().withMessage("please enter valid choice"),
    body("examID").isNumeric().withMessage("please enter valid exam id"),
    async (req, res) => {
        // validation
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return res.status(400).json({
                msg: errors
            })
        }
        // prepare object 
        const question = {
            questionText: req.body.questionText,
            choice1: req.body.choice1,
            choice2: req.body.choice2,
            choice3: req.body.choice3,
            choice4: req.body.choice4,
            rhightChoice: req.body.rhightChoice,
            examID: req.body.examID
        }
        // insert object into data base questions
        const query = util.promisify(conn.query).bind(conn);
        await query("insert into questions set ?", question);
        res.status(200).json(req.body);
    }
)


// UPDATE QUESTION
router.put("/:id", admin,
    body("questionText"),
    body("choice1"),
    body("choice2"),
    body("choice3"),
    body("choice4"),
    body("rhightChoice"),
    body("examID"),
    async (req, res) => {
        // validation
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return res.status(400).json({
                msg: errors
            })
        }
        const query = util.promisify(conn.query).bind(conn);
        const questionid = req.params.id;
        const question = await query("select * from questions where questionID = ?", [req.params.id])
        if (!question[0]) {
            res.status(404).json({
                msg: "the question not found "
            })
        }
        // prepare object 
        const questionobj = {
            questionText: req.body.questionText,
            choice1: req.body.choice1,
            choice2: req.body.choice2,
            choice3: req.body.choice3,
            choice4: req.body.choice4,
            rhightChoice: req.body.rhightChoice,
            examID: req.body.examID
        }
        // update object into data base questions

        await query("update questions set ? where questionID = ? ", [questionobj, questionid]);
        res.status(200).json(req.body);
    }
)


//DELETE QUESTION 
router.delete("/:id", admin, async (req, res) => {
    const questionid = req.params.id;
    const query = util.promisify(conn.query).bind(conn);
    const question = await query("select * from questions where questionID = ?", [questionid])
    if (!question[0]) {
        res.status(404).json({
            msg: "the question not found "
        })
    }
    else {
        const query = util.promisify(conn.query).bind(conn);
        await query("delete from questions where questionID = ? ", questionid);
        res.status(200).json({
            msg: "THE QUESTION DELETE.."
        });
    }
}
)

router.get("/getquestion/:id", async (req, res) => {
    const query = util.promisify(conn.query).bind(conn);
    const question = await query("select * from questions where questionID = ?", [
        req.params.id,
    ]);
    if (!question[0]) {
        res.status(404).json({ ms: "question not found !" });
    }
    res.status(200).json(question[0]);
});

// USER CAN GET ALL QUESTIONS 
router.get("/getall/:examid", async (req, res) => {
    exammid = req.params.examid;
    const query = util.promisify(conn.query).bind(conn);
    const questions = await query("select * from questions where examID = ?", exammid)
    res.status(200).json(questions);
})

router.get("/solveru/:id/:idd", authorized, async (req, res) => {
    examid = req.params.idd
    questionid = req.params.id;
    const query = util.promisify(conn.query).bind(conn);
    const question = await query("select * from questions where examID = ? and questionID= ?", [examid, questionid])
    res.status(200).json(question);
})




// USER CAN SOLVE QUESTION 

module.exports = router;




// questionText  choice1 choice2 choice3 choice4 rhightChoice examID
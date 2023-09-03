const express = require("express");
const app = express();
const port = 4001;

//============GLOBAL MIDWARE==============
app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(express.static("upload"));
const cors = require("cors");
app.use(cors());




//============REQUIRE MODULE API==============
const auth = require('./routes/auth');
const exams = require('./routes/exames');
const question = require('./routes/question');
const user = require('./routes/user');

//============REQUIRE MODULE API==============
app.listen(port, () => {
    console.log("SERVER IS RUNNING")
})

//============API ROUTES==============
app.use("/auth", auth);
app.use("/exam", exams);
app.use("/question", question);
app.use("/user", user);


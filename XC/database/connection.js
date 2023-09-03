const mysql = require("mysql");
const connection = mysql.createConnection({
  host: "localhost",
  user: "root",
  password: "",
  database: "earyy",
  port: "4306",
});

connection.connect((err) => {
  if (err) {
    console.log(err);
  } else {
    console.log("CONNECT WITH DATASE");
  }
});

module.exports = connection;

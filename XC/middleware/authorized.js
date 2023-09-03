const conn =require("../database/connection");
const util = require("util");


const authorized = async (req,res,next)=>{
    const { token } = req.headers;
    const query = util.promisify(conn.query).bind(conn);
    const users = await query("select * from user where token = ?",[token]);
    if(users[0])
    {
        next();
    }
    else
    {
        res.status(403).json({
            err: "you cant use this page ",
        })
    }
}

module.exports = authorized;
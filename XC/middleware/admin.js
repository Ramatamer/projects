const conn =require("../database/connection");
const util = require("util");


const admin = async (req,res,next)=>{
    const { token } = req.headers;
    const query = util.promisify(conn.query).bind(conn);
    const admin = await query("select * from user where token = ?",[token]);
    if(admin[0] && admin[0].role == 1)
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



module.exports = admin;
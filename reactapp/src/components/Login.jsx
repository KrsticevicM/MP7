import './Login.css'
import {Link} from 'react-router-dom'
import { useState } from 'react';
import { redirect } from "react-router-dom";


function Login(){

    const [username,setUsername]=useState("");
    const [password,setPassword]=useState("");

    const handlesubmit=(e)=>{
        e.preventDefault();
        const info={username,password}
        console.log(info)
        redirect("/")
        //dodat fetch i onda update stranicu s obzirom na login podatke

    }
    
    return (
        <div className="login-container">

            <section className="login-window">
                <h1 className='login-headline'>Prijava</h1>
                <form onSubmit={handlesubmit}>
                    <div className="form-floating ">
                        
                        <input type="text" 
                        className="form-control" 
                        id="floatingInput" 
                        placeholder="Username" 
                        value={username} 
                        onChange={(e)=>setUsername(e.target.value)}
                        />   
                        <label htmlFor="floatingInput">Username</label>   

                    </div>
                    <div className="form-floating">
                        
                        <input 
                        type="password" 
                        className="form-control" 
                        id="floatingPassword" 
                        placeholder="Password"
                        value={password}
                        onChange={(e)=>setPassword(e.target.value)}
                        /> 
                        <label htmlFor="floatingPassword">Password</label>
                    </div>
                    <div className='button-container'>
                        <button className="btn" id='btn'>Prijavi se</button>
                    </div>

                    <p className='nisiregistriran'> Nisi registriran? <Link to='/registration' id='link'>Registriraj se</Link></p> 
                        
                </form>
            </section>
        </div>
    );
}

export default Login;
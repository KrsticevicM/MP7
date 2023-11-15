import { useContext, useState } from 'react';
import './Login.css'
import {Form, Link, Navigate, useActionData} from 'react-router-dom'
import { redirect } from "react-router-dom";
import { AuthContext } from './AuthenticationContext';

let isPending=false
function setIsPending(value){
    isPending=value
}

function Login(){
    
    const {user,updateUser}=useContext(AuthContext)
    const [error,setError]=useState("")

    const loginAction = async(event)=>{
        event.preventDefault()

        //getting form data and turning it into object
        const data= new FormData(event.target)
    
        const submission={
            username: data.get('username'),
            password: data.get('password')
        }
        /*
        setIsPending(true)
        //send post request with fetch
        //TODO fix route to one that exists
        fetch("https://localhost:7024/api/login",{
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(submission)
        }).then((res)=>{
            console.log(submission)
            setIsPending(false)
            if(!res.ok){
                return {error: "Krivi username ili password"}
            }
            
        })
        setError("Nevaljani username ili password")
        return
        */
       
        
        console.log(submission)
        
        updateUser({firstName:"FRAN",lastName:"KUFRIN",isAuth:true,userID:32321})
        //redirect to homepage if successful
    }

    if(user.isAuth){
        return <Navigate to="/"/>
    }

    
    return (
        <div className="login-container">

            <section className="login-window">
                <h1 className='login-headline'>Prijava</h1>
                <Form onSubmit={loginAction}>
                    <div className="form-floating ">
                        
                        <input type="text" 
                        className="form-control" 
                        id="floatingInput" 
                        placeholder="Username" 
                        name='username'
                        required
                        />   
                        <label htmlFor="floatingInput">Username</label>   

                    </div>
                    <div className="form-floating">
                        
                        <input 
                        type="password" 
                        className="form-control" 
                        id="floatingPassword" 
                        placeholder="Password"
                        name='password'
                        required
                        /> 
                        <label htmlFor="floatingPassword">Password</label>
                    </div>
                    <div className='button-container'>
                        {!isPending && <button className="btn" id='btn'>Prijavi se</button>}
                        {isPending && <button className="btn" id='btn' disabled>Prijava...</button>}

                    </div>

                    <p className='nisiregistriran'> Nisi registriran? <Link to='/registration' id='link'>Registriraj se</Link></p> 
                    
                    <p id="login-error" >{error}</p>
                    
                </Form>
            </section>
        </div>
    );
}

export default Login;


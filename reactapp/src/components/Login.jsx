import './Login.css'
import {Form, Link, useActionData} from 'react-router-dom'
import { redirect } from "react-router-dom";

let isPending=false
function setIsPending(value){
    isPending=value
}

function Login(){
    const data = useActionData()
    
    return (
        <div className="login-container">

            <section className="login-window">
                <h1 className='login-headline'>Prijava</h1>
                <Form method='POST' action="/login">
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
                    {data && data.error && 
                    <p id="login-error" >{data.error}</p>
                    }
                </Form>
            </section>
        </div>
    );
}

export default Login;

export const loginAction = async({request})=>{

    //getting form data and turning it into object
    const data= await request.formData()

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
        mode: "no-cors",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(submission)
    }).then((res)=>{
        console.log(submission)
        setIsPending(false)
        if(!res.ok){
            return {error: "Krivi username ili password"}
        }
        
    })
    */

    console.log(submission)
    //redirect to homepage if successful
    return redirect('/')
}
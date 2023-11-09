import { useState } from "react";
import { Form, Link, redirect, useActionData} from "react-router-dom";
import './Registration.css'

let isPending=false
function setIsPending(value){
    isPending=value
}

function Registration(){

    const [typeOfuser,setTypeOfUser]=useState("Regular")
    const data = useActionData()
    


    return (


        <div className="registration-container">
            <section className="registration-window">

                <h1 className="registration-headline">Registracija</h1>

                <Form method="post" action="/registration">
                <div className="registration-details">
                    <div className="form-divider">
                        <div className="form-floating ">
                                    
                            <input type="text" 
                            className="form-control" 
                            id="floatingInput" 
                            placeholder="Username" 
                            name="username"
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
                            name="password"
                            required
                            /> 
                            <label htmlFor="floatingPassword">Password</label>
                        </div>
                        <div className="form-floating">
                            
                            <input 
                            type="text" 
                            className="form-control" 
                            id="floatingEmail" 
                            placeholder="Email"
                            name="email"
                            required
                            /> 
                            <label htmlFor="floatingEmail">Email</label>
                        </div>
                        
                        <div className="form-floating">
                            
                            <input 
                            type="text" 
                            className="form-control" 
                            id="floatingPhoneNumber" 
                            placeholder="PhoneNumber"
                            name="phoneNumber"
                            required
                            /> 
                            <label htmlFor="floatingPhoneNumber">Broj telefona</label>
                        </div>
                    </div>
                    <div className="form-divider">
                        <label htmlFor="typeOfUser">Vrsta korisnika </label>

                        <select
                        id="typeOfUser"
                        value={typeOfuser}
                        onChange={(e)=>setTypeOfUser(e.target.value)}
                        name="typeOfUser"
                        required
                        >
                            <option value="Shelter">Sklonište</option>
                            <option value="Regular">Korisnik</option>
                        </select>
                        
                        
                        {typeOfuser=="Regular" && 
                        <div>
                            <div className="form-floating">
                                
                                <input 
                                type="text" 
                                className="form-control" 
                                id="floatingFirstName" 
                                placeholder="FirstName"
                                name="firstName"
                                required
                                /> 
                                <label htmlFor="floatingFirstName">Ime</label>
                            </div>

                            <div className="form-floating">
                                                
                                <input 
                                type="text" 
                                className="form-control" 
                                id="floatingLastName" 
                                placeholder="LastName"
                                name="lastName"
                                required
                                /> 
                                <label htmlFor="floatingLastName">Prezime</label>
                            </div>
                        </div>
                        }
                        {typeOfuser=="Shelter" && 
                        <div>
                            <div className="form-floating">
                            <input 
                            type="text" 
                            className="form-control" 
                            id="floatingShelterName" 
                            placeholder="ShelterName"
                            name="shelterName"
                            required
                            /> 
                            <label htmlFor="floatingShelterName">Naziv skloništa</label>

                            </div>

                        </div>
                        }
                    </div>
                </div>
                <div className="registration-button-container">
                    {!isPending && <button className="btn" id="btn">Registriraj me</button>}
                    {isPending && <button className="btn" id="btn" disabled>Registriranje...</button>}   
                </div>
                
                <p className='vecregistriran'> Već si registriran? <Link to='/login' id='link'>Prijavi se</Link></p> 

                {data && data.error && 
                <p id="registration-error" >{data.error}</p>
                }
            
                </Form>
            </section>
        </div>  
    );
}
export default Registration;

export const registrationAction= async ({request})=>{


    //getting form data and turning it into object
    const data = await request.formData()
    const submission={
        username: data.get('username'),
        password: data.get('password'),
        email: data.get('email'),
        phoneNumber: data.get('phoneNumber'),
        firstName: data.get('firstName'),
        lastName: data.get('lastName'),
        shelterName: data.get('shelterName'),
        typeOfUser: data.get('typeOfUser')
    }
    

    //check if info is valid and throw error if it isnt
    
    if(submission.username.length<4){
       return {error: "Username mora sadržavati barem 4 znaka"}
    }else if(submission.password.length<6){
        return {error: "Password mora imati barem 6 znaka"}
    }else if(!(submission.email.includes("@"))){
        return {error: "Nevaljan email"}
    }else if(!(isNaN(submission.phoneNumber) === false) || submission.phoneNumber.length<7){
        return {error: "Nevaljan broj telefona"}
    }else if(!(/^[a-zA-Z]/.test(submission.firstName))){
        return {error: "Nevaljano ime"}
    }else if(!(/^[a-zA-Z]/.test(submission.lastName))){
        return {error: "Nevaljano prezime"}
    }else if(!(/^[a-zA-Z]/.test(submission.shelterName))){
        return {error: "Nevaljani naziv skloništa"}
    }

    /*
    setIsPending(true)
    //send post request with fetch
    //TODO fix route to one that exists
    fetch("https://localhost:7024/api/resgistration",{
        method: "POST",
        mode: "no-cors",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(submission)
    }).then(()=>{
        console.log(submission)
        setIsPending(false)
        
    })
    */
    console.log(submission)
    //redirect to homepage if successful
    return redirect('/')
}
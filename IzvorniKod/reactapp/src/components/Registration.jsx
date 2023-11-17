import { useContext, useState } from "react";
import { Form, Link, Navigate, redirect, useActionData, useNavigate} from "react-router-dom";
import './Registration.css'
import { AuthContext } from "./AuthenticationContext";

let isPending=false
function setIsPending(value){
    isPending=value
}

function Registration(){

    const [typeOfuser,setTypeOfUser]=useState("Regular")
    const [error,setError]=useState("")
    const {user,updateUser}=useContext(AuthContext)
    const navigate=useNavigate()

    const registrationAction= async (event)=>{

        event.preventDefault()
        //getting form data and turning it into object
        const data = new FormData(event.target)
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
        
    
        //check if info is valid and set error if it isnt
        
        if(submission.username.length<4){
           setError("Username mora sadržavati barem 4 znaka")
           return 
        }else if(submission.password.length<6){
            
            setError("Password mora imati barem 6 znaka")
            return
        }else if(!(submission.email.includes("@"))){
            
            setError("Nevaljan email")
            return
        }else if(!(isNaN(submission.phoneNumber) === false) || submission.phoneNumber.length<7){
            
            setError("Nevaljan broj telefona")
             return
        }else if(!(/^[a-zA-Z]+$/.test(submission.firstName))){
            
            setError("Nevaljano ime")
            return
        }else if(!(/^[a-zA-Z]+$/.test(submission.lastName))){
            
            setError("Nevaljano prezime")
            return
        }else if(!(/^[a-zA-Z]+$/.test(submission.shelterName))){
            
            setError("Nevaljani naziv skloništa")
            return
        }
        setError("")
    
        if(submission.typeOfUser=="Regular"){
            setIsPending(true)
            //send post request with fetch
            //TODO fix route to one that exists
            fetch(`main/register_user?usrname=${submission.username}&password=${submission.password
            }&email=${submission.email}&phoneNum=${submission.phoneNumber
            }&name=${submission.firstName}&surname=${submission.lastName}`,{
                method: "POST"
                
            }).then((res)=>{
                console.log(submission)
                setIsPending(false)
                if(res.ok){
                    navigate("/login")
                }
                
            })
        } else if (submission.typeOfUser=="Shelter"){
            setIsPending(true)
            //send post request with fetch
            //TODO fix route to one that exists
            fetch(`main/register_shelter?usrname=${submission.username}&password=${submission.password
            }&email=${submission.email}&phoneNum=${submission.phoneNumber
            }&shelterName=${submission.shelterName}`,{
                method: "POST"
                
            }).then((res)=>{
                console.log(submission)
                setIsPending(false)
                if(res.ok){
                    navigate("/login")
                }
                
            })
        }
        
        
    }
    if(user.isAuth){
        return <Navigate to="/"/>
    }

    return (


        <div className="registration-container">
            <section className="registration-window">

                <h1 className="registration-headline">Registracija</h1>

                <Form onSubmit={registrationAction}>
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

                
                <p id="registration-error" >{error}</p>
                
            
                </Form>
            </section>
        </div>  
    );
}
export default Registration;


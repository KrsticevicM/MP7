import { useState } from "react";
import { Form, Link} from "react-router-dom";
import './Registration.css'

function Registration(){

    const [typeOfuser,setTypeOfUser]=useState("Regular")


    return (


        <div className="registration-container">
            <section className="registration-window">

                <h1 className="registration-headline">Registracija</h1>

                <Form >
                <div className="registration-details">
                    <div className="form-divider">
                        <div className="form-floating ">
                                    
                            <input type="text" 
                            className="form-control" 
                            id="floatingInput" 
                            placeholder="Username" 
                            name="Username"
                            />   
                            <label htmlFor="floatingInput">Username</label>   

                        </div>
                        <div className="form-floating">
                            
                            <input 
                            type="password" 
                            className="form-control" 
                            id="floatingPassword" 
                            placeholder="Password"
                            name="Password"
                            /> 
                            <label htmlFor="floatingPassword">Password</label>
                        </div>
                        <div className="form-floating">
                            
                            <input 
                            type="text" 
                            className="form-control" 
                            id="floatingEmail" 
                            placeholder="Email"
                            name="Email"
                            /> 
                            <label htmlFor="floatingEmail">Email</label>
                        </div>
                        
                        <div className="form-floating">
                            
                            <input 
                            type="text" 
                            className="form-control" 
                            id="floatingPhoneNumber" 
                            placeholder="PhoneNumber"
                            name="PhoneNumber"
                            /> 
                            <label htmlFor="floatingPhoneNumber">Phone Number</label>
                        </div>
                    </div>
                    <div className="form-divider">
                        <label htmlFor="typeOfUser">Vrsta korisnika </label>

                        <select
                        id="typeOfUser"
                        value={typeOfuser}
                        onChange={(e)=>setTypeOfUser(e.target.value)}
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
                                name="FirstName"
                                /> 
                                <label htmlFor="floatingFirstName">Ime</label>
                            </div>

                            <div className="form-floating">
                                                
                                <input 
                                type="text" 
                                className="form-control" 
                                id="floatingLastName" 
                                placeholder="LastName"
                                name="LastName"
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
                            name="ShelterName"
                            /> 
                            <label htmlFor="floatingShelterName">Naziv skloništa</label>

                            </div>

                        </div>
                        }
                    </div>
                </div>
                <div className="registration-button-container">
                    <button className="btn" id="btn">Registriraj me</button>
                </div>
                
                <p className='vecregistriran'> Već si registriran? <Link to='/login' id='link'>Prijavi se</Link></p> 
            
                </Form>
            </section>
        </div>  
    );
}
export default Registration;
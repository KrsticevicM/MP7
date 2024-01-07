import { Form, Link, useLoaderData } from 'react-router-dom';
import './Shelter.css'
import { useState } from 'react';

function Shelter() {

  const data=useLoaderData()

  const [filtername,setFilterName] = useState("")

  const handleSubmit = async(event) =>{
    event.preventDefault()
    const formData = new FormData(event.target)
    
    setFilterName(formData.get("shelter-name"))
  }

  return (
    <div className="home-shelter-container">
      <div className="left-shelter-categories">
        <h1 className="search-heading">Pretraživanje</h1>
        <div className="categories-shelter-container">
          <Form onSubmit={handleSubmit}>
            <div className="form-floating mb-3">
              <input
                type="name"
                className="form-control"
                id="shelter-name"
                name="shelter-name"
                placeholder="Ime skloništa"
              />
              <label htmlFor="shelter-name">Ime skloništa</label>
            </div>
            <div className="btnFilter-container">
              <button type="submit" className="btn btn-light" >
                Pretraži
              </button>
            </div>
          </Form>
        </div>
      </div>
      <div className="ads-shelter-container">
        <div className="ads-shelter-container2">

          {data.filter(shelter=>{
            if(filtername=="") return true
            return shelter.nameShelter.toLowerCase().includes(filtername.toLocaleLowerCase())
          
          }).map((shelter)=>(
            <Link to={`/skloniste/${shelter.userID}`} key={shelter.nameShelter} className='shelter-display'>
              
              <h2 className='shelter-text-name'>
                {shelter.nameShelter}
              </h2>
              <div className='shelter-text-email'>
                Email: {shelter.email}
              </div>
              <div className='shelter-text-phoneNum'>
                Broj telefona: {shelter.phoneNum}
              </div>
    
            </Link>
          ))}

        </div>
      </div>
    </div>
  );
}

export const ShelterLoader = async () =>{
  const res= await fetch("/main/shelter_data")
  return res.json()
}

export default Shelter;

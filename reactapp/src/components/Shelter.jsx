import { Link, useLoaderData } from 'react-router-dom';
import './Shelter.css'

function Shelter() {

  const shelterNames=[
    {name:"Skloniste Marija",
    email: "sklonisteMarija@gmail.com",
    phonenum: "0991834523"}
    ,{name:"Skloniste Zagreb",
    email: "sklonisteZagreb@gmail.com",
    phonenum: "0994932523"}
    , {name:"PetSmart",
    email: "petsmart@gmail.com",
    phonenum: "091234023"}
    ,{name:"Skloniste Zagreb2",
    email: "sklonisteZagreb@gmail.com",
    phonenum: "0994932523"}
    , {name:"PetSmart2",
    email: "petsmart@gmail.com",
    phonenum: "091234023"},
    {name:"Skloniste Marija2",
    email: "sklonisteMarija@gmail.com",
    phonenum: "0991834523"}
    ]
    const data=useLoaderData()
    console.log(data)


  return (
    <div className="home-shelter-container">
      <div className="left-shelter-categories">
        <h1 className="search-heading">Pretraživanje</h1>
        <div className="categories-shelter-container">
          <form>
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
              <button type="submit" className="btn btn-light">
                Pretraži
              </button>
            </div>
          </form>
        </div>
      </div>
      <div className="ads-shelter-container">
        <div className="ads-shelter-container2">

          {data.map((shelter)=>(
            <Link to={`/sklonista/${shelter.userID}`} key={shelter.nameShelter} className='shelter-display'>
              
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
  const res= await fetch("main/shelter_data")
  return res.json()
}

export default Shelter;

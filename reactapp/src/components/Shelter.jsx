import { Link } from 'react-router-dom';
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

          {shelterNames.map((shelter)=>(
            <Link to="/sklonista/5" key={shelter.name} className='shelter-display'>
              
              <h2 className='shelter-text-name'>
                {shelter.name}
              </h2>
              <div className='shelter-text-email'>
                Email: {shelter.email}
              </div>
              <div className='shelter-text-phoneNum'>
                Broj telefona: {shelter.phonenum}
              </div>
                
              
            </Link>
          ))}

        </div>
      </div>
    </div>
  );
}

export default Shelter;

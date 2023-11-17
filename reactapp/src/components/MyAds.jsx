import MyAd_card from "./MyAd_card";
import "./MyAds.css";
import { Link, Navigate } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import { AuthContext } from "./AuthenticationContext";

function MyAds() {

    const { user, updateUser } = useContext(AuthContext)
    const [ads, setAds] = useState()
  
    

  return (
    <div className="myads-container">
      <br />
      {ads.map((ad) => (
        <MyAd_card
          key={ad.id}
          image={ad.image}
          petname={ad.petname}
          datehour={ad.datehour}
          age={ad.age}
          kategorija={ad.kategorija}
        />
      ))}
      <Link to="/newAd">
        <button className="btn btn-light" id="add-button">
          Dodaj oglas <i className="bi bi-plus-lg"></i>
        </button>
      </Link>
    </div>
  );
}

export default MyAds;

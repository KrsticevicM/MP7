import MyAd_card from "./MyAd_card";
import "./MyAds.css";
import { Link, Navigate } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import { AuthContext } from "./AuthenticationContext";

function MyAds() {

    const { user, updateUser } = useContext(AuthContext)
    const [ads, setAds] = useState()
    const [isPending, setPending] = useState(true)

    useEffect(() => {
        fetch('main/frontpagedata')
            .then(res => {
                return res.json();
            })
            .then(data => {
                const update_ads = [];
                const ad_ids = []
                data.Data.map((ad) => {
                    if (!(ad_ids.includes(ad.adID)) && (user.userID == ad.userID) && ad.catAd=='u potrazi') { 
                        update_ads.push(ad);
                        ad_ids.push(ad.adID);
                    }
                })
                if (update_ads.length == 0) {
                    setAds(false);
                } else {
                    setAds(update_ads);
                }
                setPending(false);
            })
    }, []);

  return (
    <div className="myads-container">
          <br />
          {isPending && <p className="loading">Loading...</p> }
          {(!ads && !isPending) && <h1>Nemate postavljenih oglasa</h1> }
      {ads && ads.map((ad) => (
        <MyAd_card
              key={ad.adID}
              image={ad.photo}
              petname={ad.namePet}
              datehour={ad.dateHourMis}
              age={ad.age}
              kategorija={ad.catAd}
              adID={ad.adID }
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

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
                const colors_arr = [];
                var colors_ad = [];
                var current_id = data.Data[0].adID;
                const update_ads = [];
                const ad_ids = [];
                const images_arr = [];
                var images_ad = [];
                console.log(data.Data);    
                data.Data.map((ad) => {
                    if (current_id != ad.adID) {
                        var obj = { key: "", value: "" };
                        var obj2 = { key: "", value: "" };
                        obj.key = current_id;
                        obj.value = colors_ad;
                        obj2.key = current_id;
                        obj2.value = images_ad;
                        colors_arr.push(obj);
                        images_arr.push(obj2);
                        colors_ad = [];
                        images_ad = [];
                        current_id = ad.adID;
                    }
                    if (!colors_ad.includes(ad.color)) {
                        colors_ad.push(ad.color);
                    }
                    if (!images_ad.includes(ad.photo)) {
                        images_ad.push(ad.photo);
                    }
                })
                colors_arr.push(colors_ad);
                data.Data.map((ad) => {
                    if (!(ad_ids.includes(ad.adID)) && (user.userID == ad.userID) && (ad.catAd != 'obrisano')) { 
                        var newElem = ad;
                        newElem['color_list'] = colors_arr.filter((item) => item.key == ad.adID)[0].value;
                        newElem['photo_list'] = images_arr.filter((item) => item.key == ad.adID)[0].value;
                        update_ads.push(newElem);
                        ad_ids.push(ad.adID);
                    }
                })
                if (update_ads.length == 0) {
                    setAds(false);
                } else {
                    setAds(update_ads);
                }
                setPending(false);
                console.log(update_ads);
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
              adID={ad.adID}
              data={ad }
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

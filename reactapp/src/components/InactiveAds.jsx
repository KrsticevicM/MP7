import Ad_card from "./Ad_card";
import "./InactiveAds.css";
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
                    if (!(ad_ids.includes(ad.adID)) && (ad.catAd != 'u potrazi')) {
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
        <div className="ads-container">
                
            {isPending && <p className="loading">Loading...</p>}
            {(!ads && !isPending) && <h1>Nema neaktivnih oglasa</h1>}
            {ads && ads.map((ad) => (
                <Link to={'/' + ad.adID} key={ad.adID}>
                    <Ad_card
                        petname={ad.namePet}
                        image={ad.photo}
                        description={ad.description}
                    />
                </Link>
            ))}
 
               
        </div>

    );
}

export default MyAds;

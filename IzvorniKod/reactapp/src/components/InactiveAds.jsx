import Ad_card from "./Ad_card";
import "./InactiveAds.css";
import { Link, Navigate } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import { AuthContext } from "./AuthenticationContext";
import ListGroup from "./ListGroup";
import useScreenSize from './screenSizeHook';

function MyAds() {
    const screenSize = useScreenSize();
    const [filter, setFilter] = useState(false)

    const { user, updateUser } = useContext(AuthContext)
    const [ads, setAds] = useState()
    const [isPending, setPending] = useState(true)

    let screen = useScreenSize();

    const searchAds = (childData) => {

        console.log(childData)

        fetch(`main/search_ad`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(JSON.stringify(childData))
        }).then(res => {
            return res.json();
        }).then(data => {
            console.log(data);
            const update_ads = [];
            const ad_ids = []
            data.Data.map((ad) => {
                if (!(ad_ids.includes(ad.adID)) && (ad.catAd != 'u potrazi' && ad.catAd != 'obrisano')) {
                    update_ads.push(ad);
                    ad_ids.push(ad.adID);
                }
            })
            if (update_ads.length == 0) {
                setAds(false);
            } else {
                setAds(update_ads);
            }
            if (screen.width <= 1024) {
                setFilter(false);
            }
            window.scrollTo(0, 0);
        })
    }

    useEffect(() => {
        fetch('main/frontpagedata')
            .then(res => {
                return res.json();
            })
            .then(data => {
                const update_ads = [];
                const ad_ids = []
                data.Data.map((ad) => {
                    if (!(ad_ids.includes(ad.adID)) && (ad.catAd != 'u potrazi' && ad.catAd != 'obrisano')) {
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
        <div className="home-container">
            <button className="filter-button" onClick={() => setFilter(!filter)}>Filter  <i className="bi bi-funnel"></i></button>
            {(filter || screenSize.width > 1024) && <div className="left-categories">
                <h1 className="search-heading">Pretraživanje</h1>
                <div className="categories-container">
                    <ListGroup parentCallback={searchAds} />
                </div>
            </div>}
            <div className="inactive-ads-container">
                <div className="inactive-ads-container2">
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
            </div>
      </div>
  );
}

export default MyAds;

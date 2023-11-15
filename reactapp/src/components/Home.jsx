import "./Home.css";
import Ad_card from "./Ad_card";
import ListGroup from "./ListGroup";
import { Link } from 'react-router-dom'
import useFetch from "./useFetch"
import { useEffect, useState } from "react";

function Home() {

    const [ads, setAds] = useState()

    useEffect(() => {
        fetch('main')
            .then(res => {
                return res.json();
            })
            .then(data => {
                console.log(data.Data);
                setAds(data.Data);
                console.log(btoa(data.Data[0].photo));
            })
    }, []);
        
  return (
    <div className="home-container">
      <div className="left-categories">
        <h1 className="search-heading">Pretraživanje</h1>
        <div className="categories-container">
          <ListGroup />
        </div>
      </div>
      <div className="ads-container">
              <div className="ads-container2">
                  {ads && ads.map((ad) => (
                      <Link to={'/'+ad.adID} key={ad.adID}>
              <Ad_card
                petname={ad.namePet}
                image={btoa(ad.photo)}
                description={ad.description}
              />
            </Link>
          ))}
        </div>
      </div>
    </div>
  );
}

export default Home;

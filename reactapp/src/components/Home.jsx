import "./Home.css";
import Ad_card from "./Ad_card";
import ListGroup from "./ListGroup";
import { Link } from 'react-router-dom'
import useFetch from "./useFetch"
import { useEffect } from "react";

function Home() {

    const { data: ads_test, isPending, Error } = useFetch('main');
        
  const ads = [
    {
      image: "images/Bichon-frise-dog.webp",
      petname: "Johnny",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Johnny",
      id: 1,
    },
    {
      image:
        "images/black-dog-breeds-black-labrador-retriever-1566497968.jpg",
      petname: "Timmy",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Timmy",
      id: 2,
    },
    {
      image:
        "images/dog-puppy-on-garden-royalty-free-image-1586966191.jpg",
      petname: "Samuel",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Samuel",
      id: 3,
    },
    {
      image: "images/gettyimages-1190158957-1040x690.jpg",
      petname: "Willy",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Willy",
      id: 4,
    },
    {
      image: "images/preuzmi.jpg",
      petname: "Armando",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Armando",
      id: 5,
    },
    {
      image: "images/images (1).jpg",
      petname: "Cillian",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Cillian",
      id: 6,
    },
    {
      image: "images/PomeranianKardashianpoms.jpg",
      petname: "Rocky",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Rocky",
      id: 7,
    },
    {
      image: "images/images.jpg",
      petname: "Jimmy",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Jimmy",
      id: 8,
    },
    {
      image: "images/Portrait-of-a-brown-lagotto-romagnolo.jpg",
      petname: "Pippo",
      description:
        "Izgubljen 21.01.2023, posljednja lokacija sutomiska ulica, odaziva se na ime Pippo",
      id: 9,
    },
  ];
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
                  {ads.map((ad) => (
                      <Link to={'/'+ad.id} key={ad.id}>
              <Ad_card
                petname={ad.petname}
                image={ad.image}
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

import Ad_card from './Ad_card.jsx';
import ListGroup from './ListGroup.jsx';
import './Home.css'


function Home() {
    return (
        <div className='home-container'>
            <div className='left-categories'>
                <h1 className='search-heading'>Pretraživanje</h1>
                <div className='categories-container'>
                    <ListGroup />
                </div>
            </div>
            <div className='ads-container'>
                <div className='ads-container2'>
                    <Ad_card name="Johnny" image="\images\Bichon-frise-dog.webp" />
                    <Ad_card name="Timmy" image="\images\black-dog-breeds-black-labrador-retriever-1566497968.jpg" />
                    <Ad_card name="Samuel" image="\images\dog-puppy-on-garden-royalty-free-image-1586966191.jpg" />
                    <Ad_card name="Willy" image="\images\gettyimages-1190158957-1040x690.jpg" />
                    <Ad_card name="Philip" image="\images\preuzmi.jpg" />
                    <Ad_card name="Cillian" image="\images\images (1).jpg" />
                    <Ad_card name="Barthy" image="\images\PomeranianKardashianpoms.jpg" />
                    <Ad_card name="Victor" image="\images\Portrait-of-a-brown-lagotto-romagnolo.jpg" />
                    <Ad_card name="Robert" image="\images\images.jpg" />
                </div>
            </div>
        </div>
    );
}

export default Home;
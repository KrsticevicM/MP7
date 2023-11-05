import './ListGroup.css'


function ListGroup() {
    return (
        <>
            <div className="dropdown">
                <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    <label className="Animal-type">Vrsta ljubimca</label>
                </button>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a className="dropdown-item" href="#">Pas</a>
                    <a className="dropdown-item" href="#">Mačka</a>
                    <a className="dropdown-item" href="#">Papiga</a>
                </div>
            </div>
            <label className="pet-name">
                Ime ljubimca: <input name="myInput" />
            </label>
            <label>Datum i vrijeme nestanka:<input type="datetime-local" name="datetime" /></label>
        </>
    );
}

export default ListGroup;
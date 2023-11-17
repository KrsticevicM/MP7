function Shelter() {
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
        <div className="ads-shelter-container2"></div>
      </div>
    </div>
  );
}

export default Shelter;

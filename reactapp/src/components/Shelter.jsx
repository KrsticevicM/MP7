function Shelter() {
  return (
    <div className="home-container">
      <div className="left-categories">
        <h1 className="search-heading">Pretraživanje</h1>
        <div className="categories-container">
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
      <div className="ads-container">
        <div className="ads-container2"></div>
      </div>
    </div>
  );
}

export default Shelter;

import "./Navbar.css";

function Navbar() {
  return (
    <div className="navbar-container">
      <img
        alt="logo"
        src="images\Dog_Paw_Print.png"
        className="logo-img"
      />
      <div className="navbar-inner-container">
        <div className="element-container">POČETNA</div>
        <div className="element-container">SKLONIŠTA</div>
        <div className="element-container">PRIJAVI SE</div>
      </div>
    </div>
  );
}

export default Navbar;

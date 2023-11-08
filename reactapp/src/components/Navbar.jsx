import "./Navbar.css";
import {Link} from 'react-router-dom'
function Navbar() {
  return (
    <div className="navbar-container">
      <img
        alt="logo"
        src="images\Dog_Paw_Print.png"
        className="logo-img"
      />
      <div className="navbar-inner-container">
              <Link to='/'><div className="element-container">POČETNA</div></Link>
        <div className="element-container">SKLONIŠTA</div>
              <Link to='/login'><div className="element-container">PRIJAVI SE</div></Link>
      </div>
    </div>
  );
}

export default Navbar;

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
        <Link className="element-container" to='/'>POČETNA</Link>
        <div className="element-container">SKLONIŠTA</div>
        <Link className="element-container" to='/login'>PRIJAVI SE</Link>
      </div>
    </div>
  );
}

export default Navbar;

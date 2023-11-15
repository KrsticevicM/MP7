import { useContext } from "react";
import "./Navbar.css";
import {Link} from 'react-router-dom'
import { AuthContext } from "./AuthenticationContext";
import { Button } from "bootstrap";
function Navbar() {
  const {user,updateUser}=useContext(AuthContext)
  const eventHandler=()=>{
    updateUser({
      isAuth: false,
      userID: null,
      firstName: '',
      lastName: '',
    })
  }

  return (
    <div className="navbar-container">
      <img
        alt="logo"
        src="images\Dog_Paw_Print.png"
        className="logo-img"
      />
      <div className="navbar-inner-container">
              <Link to='/'><div className="element-container">POČETNA</div></Link>
              <Link to='/sklonista'><div className="element-container">SKLONIŠTA</div></Link>
              {!user.isAuth && <Link to='/login'><div className="element-container">PRIJAVI SE</div></Link>}
              {user.isAuth && <Link to='/'><div className="element-container">{user.firstName} {user.lastName}</div></Link>}
              {user.isAuth && <button className="odjava-button" onClick={eventHandler}>ODJAVA</button>}
      </div>
    </div>
  );
}

export default Navbar;

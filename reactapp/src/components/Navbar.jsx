import { useContext, useEffect } from "react";
import "./Navbar.css";
import {Link} from 'react-router-dom'
import { AuthContext } from "./AuthenticationContext";
import { Button } from "bootstrap";
import { useState } from "react";
function Navbar() {

    const [menuState, setMenuState] = useState(false);

  const {user,updateUser}=useContext(AuthContext)
    const eventHandler = () => {
        setMenuState(false)
    updateUser({
      isAuth: false,
      userID: null,
      firstName: '',
      lastName: '',
    })
      window.location.href = '/'
  }

    return (
      <>
    <div className="navbar-container">
      <img
        alt="logo"
        src="images\Dog_Paw_Print.png"
        className="logo-img"
      />
                <div className="navbar-inner-container">
                    <button className="navbar-menu-button" onClick={() => setMenuState(!menuState)}><i className="bi bi-list"></i></button>
              <Link to='/'><div className="element-container">POČETNA</div></Link>
              <Link to='/sklonista'><div className="element-container">SKLONIŠTA</div></Link>
              {!user.isAuth && <Link to='/login'><div className="element-container">PRIJAVI SE</div></Link>}
              {user.isAuth && <Link to='/moji-oglasi'><div className="element-container">{user.firstName} {user.lastName}</div></Link>}
              {user.isAuth && <button className="odjava-button" onClick={eventHandler}>ODJAVA</button>}
      </div>
            </div>
            {menuState && < div className="navbar-popup-menu">
                <Link to='/'><div className="element-container-menu" onClick={() => setMenuState(false)}>POČETNA</div></Link>
                <Link to='/sklonista'><div className="element-container-menu" onClick={() => setMenuState(false)}>SKLONIŠTA</div></Link>
                {!user.isAuth && <Link to='/login'><div className="element-container-menu" onClick={() => setMenuState(false)}>PRIJAVI SE</div></Link>}
                {user.isAuth && <Link to='/moji-oglasi'><div className="element-container-menu" onClick={() => setMenuState(false)}>{user.firstName} {user.lastName}</div></Link>}
                {user.isAuth && <button className="odjava-button-menu" onClick={eventHandler}>ODJAVA</button>}
            </div>}
        </>
  );
}

export default Navbar;

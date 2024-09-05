import axios from "axios";
import { useEffect, useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { API_URL, baseURL } from './constants/base';

function ProtectedRoutes() {
  const [isLogged, setIsLogged] = useState(false);
  const [waiting, setWaiting] = useState(true);

  const logout = async () => {
    const response = await axios.get(baseURL + API_URL + "/logout", {
      withCredentials: true
    });
    const data = response.data;
    if (response.status === 200) {
      localStorage.removeItem("user");
      alert(data.message);
      window.location.href = "/login";
    } else {
      console.log("could not logout: ", response);
    }
  };

  useEffect(() => {
    axios.get(baseURL + API_URL + '/xhtlekd', {
      withCredentials: true
    }).then(response => {

      if (response.status === 200 && response.data.user && response.data.user.email) {
        setIsLogged(true);
        localStorage.setItem("user", response.data.user.email);
      } else {
        setIsLogged(false);
      }
      // return response.data;

    }).catch(err => {
      if (err.response && err.response.status === 403) {
        console.log("Error protected routes: ", err);
      }
      setIsLogged(false);
      localStorage.removeItem("user");
    }).finally(() => {
      setWaiting(false);
    });

  }, []);
  return waiting ? (
    <div className="waiting-page">
      <div>Waiting...</div>
    </div>
  ) : (
    isLogged ? <section>
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container">
          <div className="d-flex w-100" style={{ justifyContent: "space-between" }}>
            <ul className="navbar-nav">
              <li className="nav-item">
                <a className="nav-link" aria-current="page" href="/">Home</a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="/admin">Admin</a>
              </li>

            </ul>
            <button onClick={logout} className="btn btn-danger">Log Out</button>

          </div>
        </div>
      </nav>
      <div className="container py-5">
        <Outlet />
      </div>
    </section> : <Navigate to="/login" />
  );
}
export default ProtectedRoutes;
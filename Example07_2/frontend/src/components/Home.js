import axios from 'axios';
import { useEffect, useState } from 'react';
import { API_URL, baseURL } from '../constants/base';

function Home() {
  document.title = "Welcome";
  const [userInfo, setUserInfo] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const user = localStorage.getItem("user");
    if (user) {
      axios.get(`${baseURL}${API_URL}/home/${user}`, {
        withCredentials: true,
      })
      .then(response => response.data)
      .then(data => {
        setUserInfo(data.userInfo);
        setLoading(false);
      })
      .catch(error => {
        console.log("Error home page: ", error);
        setLoading(false);
      });
    } else {
      setLoading(false);
    }
  }, []);

  return (
    <section className='page'>
      <header className='text-center mb-4'>
        <h1>Welcome to Your Page</h1>
      </header>

      {loading ? (
        <div className='loading'>Loading...</div>
      ) : userInfo ? (
        <div className='card shadow-lg p-4'>
          <h2>User Information</h2>
          <table className='table'>
            <thead>
              <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Created Date</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>{userInfo.name}</td>
                <td>{userInfo.email}</td>
                <td>{userInfo.createdDate ? userInfo.createdDate.split("T")[0] : ""}</td>
              </tr>
            </tbody>
          </table>
        </div>
      ) : (
        <div className='warning text-center'>
          <h2>Access Denied!!!</h2>
        </div>
      )}
    </section>
  );
}

export default Home;

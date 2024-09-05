import axios from 'axios';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { API_URL, baseURL } from '../constants/base';

function Login() {
  document.title = "Login";
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  
  useEffect(() => {
    const user = localStorage.getItem("user");
    if (user) {
      navigate('/');
    }
  }, [navigate]);

  async function loginHandler(e) {
    e.preventDefault();
    const form_ = e.target;
    const submitter = document.querySelector("input.login");
    const formData = new FormData(form_, submitter);
    const dataToSend = {};
    formData.forEach((value, key) => {
      dataToSend[key] = value === "on" ? true : value;
    });

    if (dataToSend.remember === "on") {
      dataToSend.remember = true;
    }

    setLoading(true);

    try {
      const response = await axios.post(`${baseURL}${API_URL}/login`, dataToSend, { withCredentials: true });
      if (response.status === 200) {
        navigate('/');
      }
    } catch (error) {
      alert("Wrong account or password");
    } finally {
      setLoading(false);
    }
  }

  return (
    <section className='login-page-wrapper page'>
      <div className='container py-5'>
        <div className='login-page w-50 m-auto shadow-lg p-4 rounded'>
          <header className='text-center mb-4'>
            <h1>Login</h1>
          </header>
          <p className='message'></p>
          <div className='form-holder w-100'>
            <form action="#" className='login' onSubmit={loginHandler}>
              <label htmlFor="email" className='form-label'>Email</label>
              <input className='form-control mb-3' type="email" name='email' id='email' placeholder='Enter your email' required />
              
              <label htmlFor="password" className='form-label'>Password</label>
              <input className='form-control mb-3' type="password" name='password' id='password' placeholder='Enter your password' required />
              
              <div className="form-check mb-3">
                <input className="form-check-input" type="checkbox" name='remember' id='remember' />
                <label htmlFor="remember" className='form-check-label'>Remember Password?</label>
              </div>
              
              <button type="submit" className='login btn btn-primary w-100' disabled={loading}>
                {loading ? 'Logging in...' : 'Login'}
              </button>
            </form>
          </div>
          <div className='my-5 text-center'>
            <span>Or </span>
            <a href="/register">Register</a>
          </div>
        </div>
      </div>
    </section>
  );
}

export default Login;

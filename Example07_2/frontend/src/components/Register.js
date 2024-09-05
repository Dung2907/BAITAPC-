import axios from "axios";
import { useEffect } from 'react';
import { API_URL, baseURL } from '../constants/base';

function Register() {
  document.title = "Register";
  // dont ask an already registered user to register over and over again
  useEffect(() => {
    const user = localStorage.getItem("user");
    if (user) {
      document.location = "/";
    }
  }, []);
  return (
    <section className='register-page-wrapper page container py-5'>
      <div className='register-page w-50 m-auto'>
        <header className="text-center">
          <h1>Register Page</h1>
        </header>
        <p className='message'></p>
        <div className='form-holder w-100'>
          <form action="#" className='register' onSubmit={registerHandler}>
            <label htmlFor="name">Name</label>
            <br />
            <input className='form-control' type="text" name='Name' id='name' required />
            <br />
            <label htmlFor="email">Email</label>
            <br />
            <input className='form-control' type="email" name='Email' id='email' required />
            <br />
            <label htmlFor="password">Password</label>
            <br />
            <input className='form-control' type="password" name='PasswordHash' id='password' required />
            <br />
            <input type="submit" value="Register" className='register btn btn-primary' />
          </form>
        </div>
        <div className='my-5'>
          <span>Or </span>
          <a href="/login">Login</a>
        </div>
      </div>
    </section>
  );
  async function registerHandler(e) {
    e.preventDefault();
    const form_ = e.target;
    const submitter = document.querySelector("input.login");
    const formData = new FormData(form_, submitter);
    const dataToSend = {};
    for (const [key, value] of formData) {
      dataToSend[key] = value;
    }
    // create username
    const newUserName = dataToSend.Name.trim().split(" ");
    dataToSend.UserName = newUserName.join("");
    try {
      const response = await axios.post(`${baseURL}${API_URL}/register`, dataToSend, {
        withCredentials: true,
      });
      const data = response?.data;
      if (response.status === 200) {
        document.location = "/login";
      }
    } catch (error) {
      const errors = error?.response?.data?.errors
      alert(errors[0]?.description)
    }

  }
}
export default Register;
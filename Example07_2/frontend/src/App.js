import { Route, RouterProvider, createBrowserRouter, createRoutesFromElements } from 'react-router-dom';
import './App.css';
import Admin from './components/Admin';
import Home from './components/Home';
import Login from './components/Login';
import Register from './components/Register';
import ProtectedRoutes from './ProtectedRoutes';

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path='/'>
      <Route element={<ProtectedRoutes />}>
        <Route path='/' element={<Home />} />
        <Route path='/admin' element={<Admin />} />
      </Route>
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='*' element={
        <div>
          <header>
            <h1>Not Found</h1>
          </header>
          <p>
            <a href="/">Back to Home</a>
          </p>
        </div>
      } />
    </Route>
  )
);
function App() {

  return <RouterProvider router={router} />

}
export default App;
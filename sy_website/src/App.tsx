import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Home from "./pages/Home";
import Navbar from "./components/navbar/Navbar";
import Footer from './components/footer/Footer';
import Hero from './components/hero/Hero';
import Gallery from './components/gallery/Gallery';
import About from './components/about/About';


export default function App() {
  return (
    <>
      <Navbar />
      <Hero />
      <Gallery />
      <About />
      <Footer />
      {/* <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
      </BrowserRouter> */}
    </>
  );
}


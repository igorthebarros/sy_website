import './App.css'

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
      <About />
      <Gallery />
      <Footer />
    </>
  );
}


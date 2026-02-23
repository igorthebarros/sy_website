import { motion } from "framer-motion";
import "./Navbar.css";

export default function Navbar() {
  return (
    <motion.nav
      className="navbar"
      initial={{ y: -80 }}
      animate={{ y: 0 }}
    >
      <h2 className="gold">Syella Pinheiro</h2>

      <div className="nav-links">
        <a href="#gallery">Gallery</a>
        <a href="#about">About</a>
        <a href="#contact">Contact</a>
      </div>
    </motion.nav>
  );
}
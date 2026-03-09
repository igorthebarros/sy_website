import { motion } from "framer-motion";
import "./Navbar.css";

export default function Navbar() {
  return (
    <motion.nav
      className="navbar"
      initial={{ y: -80 }}
      animate={{ y: 0 }}
    >
      <h2 className="gold brand"></h2>

      <div className="nav-links">
        <a href="#about">Sobre mim</a>
        <a href="#gallery">Projetos</a>
        <a href="#contact">Contato</a>
      </div>
    </motion.nav>
  );
}
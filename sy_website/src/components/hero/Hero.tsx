import { motion } from "framer-motion";
import "./Hero.css";

export default function Hero() {
  return (
    <section
      className="hero"
      style={{
        backgroundImage: "url('/sponge.png')",
      }}
    >
      <motion.div
        className="hero-content fade-in"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ duration: 1.2 }}
      >
        <h1 className="hero-title">Syella Pinheiro</h1>
        <p className="hero-subtitle">Casamentos • Aniversários • Infantil</p>
      </motion.div>
    </section>
  );
}
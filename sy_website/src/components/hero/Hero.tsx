import { motion } from "framer-motion";
import "./Hero.css";

export default function Hero() {
  return (
    <section
      className="hero"
      style={{
        backgroundImage: "url('/hero.jpg')",
      }}
    >
      <motion.div
        className="hero-content fade-in"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ duration: 1.2 }}
      >
        <h1 className="text-6xl mb-6">Capturing Moments</h1>
        <p className="text-lg opacity-80">Wedding • Portrait • Lifestyle</p>
      </motion.div>
    </section>
  );
}
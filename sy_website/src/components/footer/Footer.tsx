import { FaEnvelope, FaInstagram, FaWhatsapp } from "react-icons/fa6";
import "./Footer.css";

export default function Footer() {
  return (
    <footer className="footer">
      <p className="opacity-70"> © {new Date().getFullYear()} SY Photography </p>
      <div className="footer-icons">
        <a
          href="https://instagram.com/yourwife"
          target="_blank"
          rel="noreferrer"
        >
          <FaInstagram />
        </a>

        <a
          href="https://wa.me/5511999999999"
          target="_blank"
          rel="noreferrer"
        >
          <FaWhatsapp />
        </a>

        <a href="mailto:email@email.com">
          <FaEnvelope />
        </a>
      </div>
    </footer>
  );
}
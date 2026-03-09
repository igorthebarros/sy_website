import { FaEnvelope, FaInstagram, FaWhatsapp } from "react-icons/fa6";
import "./Footer.css";

export default function Footer() {
  return (
    <footer className="footer">
      <div className="footer-icons">
        <a
          href="https://instagram.com/syellapinheiro.co"
          target="_blank"
          rel="noreferrer"
        >
          <FaInstagram />
        </a>

        <a
          href="https://wa.me/5541988258888"
          target="_blank"
          rel="noreferrer"
        >
          <FaWhatsapp />
        </a>

        <a href="mailto:syellapinheiro@gmail.com">
          <FaEnvelope />
        </a>
      </div>
      <p className="footer-copy opacity-70">© {new Date().getFullYear()} Syella Pinheiro Co.</p>
    </footer>
  );
}
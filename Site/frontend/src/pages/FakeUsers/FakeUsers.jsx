import { useState } from "react";
import { api } from "../../services/api";
import "./FakeUsers.css";

export default function FakeUsers() {
  const [quantity, setQuantity] = useState(5);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [file, setFile] = useState(null);
  const [fileName, setFileName] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [sendingEmail, setSendingEmail] = useState(false);

  const [email, setEmail] = useState({
    recipient: "",
    subject: "",
    body: "",
  });

  async function generateUsers() {
    setError("");
    setLoading(true);
    setFile(null);

    try {
      const response = await api.get(`/FakeDataGeneration/${quantity}`, {
        responseType: "blob",
      });

      const blob = new Blob([response.data], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      });

      setFile(blob);
      setFileName("usuarios_fake.xlsx");
    } catch {
      setError("Erro ao gerar o arquivo.");
    } finally {
      setLoading(false);
    }
  }

  function downloadFile() {
    if (!file) return;

    const url = URL.createObjectURL(file);
    const link = document.createElement("a");

    link.href = url;
    link.download = fileName;
    link.click();

    URL.revokeObjectURL(url);
  }

  function openEmailModal() {
    if (!file) {
      alert("Gere o arquivo antes de enviar o e-mail.");
      return;
    }
    setShowModal(true);
  }

  function closeModal() {
    setShowModal(false);
    setEmail({ recipient: "", subject: "", body: "" });
  }

  async function sendEmail() {
    if (!file) return;
    setSendingEmail(true);
    
    try {
      const formData = new FormData();

      formData.append("fileName", fileName);
      formData.append("attachment", file, fileName);

      await api.post("/Email/send", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      alert("E-mail enviado com sucesso");
      closeModal();
    } catch (err) {
      console.error(err);
      alert("Erro ao enviar e-mail");
    } finally {
      setSendingEmail(false);
    }
  }

  return (
    <div className="fake-users-container">
      <div className="fake-users-card">
        <div className="header">
          <div className="header-icon">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/>
              <circle cx="9" cy="7" r="4"/>
              <path d="M23 21v-2a4 4 0 0 0-3-3.87"/>
              <path d="M16 3.13a4 4 0 0 1 0 7.75"/>
            </svg>
          </div>
          <h2>Gerar Usuários Fake</h2>
          <p>Crie arquivos Excel com dados fictícios de usuários</p>
        </div>

        <div className="generator-section">
          <div className="quantity-input">
            <label htmlFor="quantity">Quantidade de usuários</label>
            <div className="input-group">
              <input
                id="quantity"
                type="number"
                min="1"
                max="10000"
                value={quantity}
                onChange={(e) => setQuantity(e.target.value)}
              />
              <button
                onClick={generateUsers}
                className="generate-button"
                disabled={loading}
              >
                {loading ? (
                  <>
                    <div className="spinner"></div>
                    Gerando...
                  </>
                ) : (
                  <>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                      <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/>
                      <polyline points="7 10 12 15 17 10"/>
                      <line x1="12" y1="15" x2="12" y2="3"/>
                    </svg>
                    Gerar Arquivo
                  </>
                )}
              </button>
            </div>
          </div>

          {error && (
            <div className="error-message">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                <circle cx="12" cy="12" r="10"/>
                <line x1="12" y1="8" x2="12" y2="12"/>
                <line x1="12" y1="16" x2="12.01" y2="16"/>
              </svg>
              {error}
            </div>
          )}

          {file && (
            <div className="file-preview">
              <div className="file-icon">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/>
                  <polyline points="14 2 14 8 20 8"/>
                  <line x1="16" y1="13" x2="8" y2="13"/>
                  <line x1="16" y1="17" x2="8" y2="17"/>
                  <polyline points="10 9 9 9 8 9"/>
                </svg>
              </div>
              <div className="file-info">
                <strong>Arquivo gerado com sucesso!</strong>
                <p>{fileName}</p>
              </div>
              <div className="file-actions">
                <button onClick={downloadFile} className="download-button">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/>
                    <polyline points="7 10 12 15 17 10"/>
                    <line x1="12" y1="15" x2="12" y2="3"/>
                  </svg>
                  Download
                </button>
                <button onClick={sendEmail} className="email-button">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/>
                    <polyline points="22,6 12,13 2,6"/>
                  </svg>
                  Enviar por E-mail
                </button>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
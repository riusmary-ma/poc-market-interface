import { useState } from "react";
import "./App.css";

const EMatchResult = {
  ExactMatch: "ExactMatch",
  CloseMatch: "CloseMatch",
  NoMatch: "NoMatch",
};

const initialData = {
  marketCode: "",
  price: "",
  quantity: "",
  amount: "",
};

const parseNumber = (value) => {
  const number = Number(value);
  return Number.isFinite(number) ? number : NaN;
};

const isValidNumber = (value) => {
  return typeof value === "string" && value.trim() !== "" && Number.isFinite(Number(value));
};

const API_MATCH_URL = "http://localhost:5000/api/match";

const validatePayload = (instruction, allegement) => {
  const instructionPrice = parseNumber(instruction.price);
  const instructionQuantity = parseNumber(instruction.quantity);
  const instructionAmount = parseNumber(instruction.amount);
  const allegementPrice = parseNumber(allegement.price);
  const allegementQuantity = parseNumber(allegement.quantity);
  const allegementAmount = parseNumber(allegement.amount);

  if (
    !instruction.marketCode ||
    !allegement.marketCode ||
    !isValidNumber(instruction.price) ||
    !isValidNumber(instruction.quantity) ||
    !isValidNumber(instruction.amount) ||
    !isValidNumber(allegement.price) ||
    !isValidNumber(allegement.quantity) ||
    !isValidNumber(allegement.amount)
  ) {
    return {
      valid: false,
      message: "请输入所有字段，并确保数量/价格/金额为有效数字。",
    };
  }

  return { valid: true };
};

const mapMatchResult = (matchData) => {
  if (matchData == null) {
    return null;
  }

  if (typeof matchData === "string") {
    return matchData;
  }

  const numericResult = Number(matchData);
  switch (numericResult) {
    case 1:
      return EMatchResult.ExactMatch;
    case 2:
      return EMatchResult.CloseMatch;
    case 3:
      return EMatchResult.NoMatch;
    default:
      return `Unknown(${matchData})`;
  }
};

function App() {
  const [instruction, setInstruction] = useState(initialData);
  const [allegement, setAllegement] = useState(initialData);
  const [matchOutput, setMatchOutput] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleInputChange = (section, field) => (event) => {
    const value = event.target.value;
    if (section === "instruction") {
      setInstruction((prev) => ({ ...prev, [field]: value }));
    } else {
      setAllegement((prev) => ({ ...prev, [field]: value }));
    }
  };

  const handleMatch = async () => {
    setError("");
    setMatchOutput(null);

    const validation = validatePayload(instruction, allegement);
    if (!validation.valid) {
      setError(validation.message);
      return;
    }

    setLoading(true);
    try {
      const payload = {
        instruction: {
          ...instruction,
          price: parseNumber(instruction.price),
          quantity: parseNumber(instruction.quantity),
          amount: parseNumber(instruction.amount),
        },
        allegement: {
          ...allegement,
          price: parseNumber(allegement.price),
          quantity: parseNumber(allegement.quantity),
          amount: parseNumber(allegement.amount),
        },
      };

      const response = await fetch(API_MATCH_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      const rawText = await response.text();
      const contentType = response.headers.get("content-type") || "";
      if (!response.ok) {
        throw new Error(rawText || "后端匹配服务调用失败。返回非 2xx 响应。");
      }

      const looksLikeJson = /^\s*[\{\[]/.test(rawText);
      if (!contentType.includes("application/json") && !looksLikeJson) {
        const preview = rawText.slice(0, 30000).replace(/\s+/g, " ");
        throw new Error(
          `后端返回非 JSON 响应，请检查 API 路径是否正确。响应内容预览：${preview}`
        );
      }

      let responseData;
      try {
        responseData = rawText ? JSON.parse(rawText) : {};
      } catch (parseError) {
        throw new Error(`后端返回的 JSON 无法解析：${parseError.message}`);
      }

      const matchedResult = mapMatchResult(responseData.matchData ?? responseData.matchResult ?? responseData.result);
      const message = responseData.message || matchedResult || "后端返回未知结果。";

      setMatchOutput({ result: matchedResult, message });
    } catch (err) {
      setError(err.message || "请求后端匹配服务时发生错误。");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Instruction / Allegement Matcher</h1>
        <p>输入 Instruction 和 Allegement 的字段，然后点击 Match 查看匹配结果。</p>

        <div className="match-grid">
          <section className="match-card">
            <h2>Instruction</h2>
            <label>
              MarketCode
              <input
                type="text"
                value={instruction.marketCode}
                onChange={handleInputChange("instruction", "marketCode")}
                placeholder="例如 ABC"
              />
            </label>
            <label>
              Price
              <input
                type="number"
                step="0.01"
                value={instruction.price}
                onChange={handleInputChange("instruction", "price")}
                placeholder="例如 100"
              />
            </label>
            <label>
              Quantity
              <input
                type="number"
                step="1"
                value={instruction.quantity}
                onChange={handleInputChange("instruction", "quantity")}
                placeholder="例如 10"
              />
            </label>
            <label>
              Amount
              <input
                type="number"
                step="0.01"
                value={instruction.amount}
                onChange={handleInputChange("instruction", "amount")}
                placeholder="例如 1000"
              />
            </label>
          </section>

          <section className="match-card">
            <h2>Allegement</h2>
            <label>
              MarketCode
              <input
                type="text"
                value={allegement.marketCode}
                onChange={handleInputChange("allegement", "marketCode")}
                placeholder="例如 ABC"
              />
            </label>
            <label>
              Price
              <input
                type="number"
                step="0.01"
                value={allegement.price}
                onChange={handleInputChange("allegement", "price")}
                placeholder="例如 100"
              />
            </label>
            <label>
              Quantity
              <input
                type="number"
                step="1"
                value={allegement.quantity}
                onChange={handleInputChange("allegement", "quantity")}
                placeholder="例如 10"
              />
            </label>
            <label>
              Amount
              <input
                type="number"
                step="0.01"
                value={allegement.amount}
                onChange={handleInputChange("allegement", "amount")}
                placeholder="例如 1000"
              />
            </label>
          </section>
        </div>

        <button className="match-button" onClick={handleMatch} disabled={loading}>
          {loading ? "Matching..." : "Match"}
        </button>

        <div className="result-box">
          {error ? (
            <div className="error-message">{error}</div>
          ) : matchOutput ? (
            <>
              <strong>Result:</strong> {matchOutput.message}
              <div className="result-detail">
                {matchOutput.result && `EMatchResult.${matchOutput.result}`}
              </div>
            </>
          ) : (
            <span>请填入数据后点击 Match。</span>
          )}
        </div>
      </header>
    </div>
  );
}

export default App;

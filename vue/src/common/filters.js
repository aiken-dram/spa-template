const DATEFORMAT = {
  year: "numeric",
  month: "numeric",
  day: "numeric",
};

const DATETIMEFORMAT = {
  year: "numeric",
  month: "numeric",
  day: "numeric",
  hour: "2-digit",
  minute: "2-digit",
};

const TIMEFORMAT = {
  hour: "2-digit",
  minute: "2-digit",
  second: "2-digit",
};

function formatMoney(amount, decimalCount = 2, decimal = ".", thousands = ",") {
  try {
    decimalCount = Math.abs(decimalCount);
    decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

    const negativeSign = amount < 0 ? "-" : "";

    let i = parseInt(
      (amount = Math.abs(Number(amount) || 0).toFixed(decimalCount))
    ).toString();
    let j = i.length > 3 ? i.length % 3 : 0;

    return (
      negativeSign +
      (j ? i.substr(0, j) + thousands : "") +
      i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) +
      (decimalCount
        ? decimal +
          Math.abs(amount - i)
            .toFixed(decimalCount)
            .slice(2)
        : "")
    );
  } catch {
    //console.log(e);
  }
}

String.prototype.lpad = function (padString, length) {
  var str = this;
  while (str.length < length) str = padString + str;
  return str;
};

export default {
  sum: (sum) => {
    return `${formatMoney(sum ? sum : 0, 2, ",", " ")} р.`;
  },
  time: (time) => {
    return time ? new Date(time).toLocaleString("ru-RU", TIMEFORMAT) : null;
  },
  bool: (bool) => {
    return bool == 1 ? "yes" : bool == 0 ? "no" : null;
  },
  datetime: (datetime) => {
    return datetime
      ? new Date(datetime).toLocaleString("ru-RU", DATETIMEFORMAT)
      : null;
  },
  date: (date) => {
    return date ? new Date(date).toLocaleString("ru-RU", DATEFORMAT) : null;
  },
};

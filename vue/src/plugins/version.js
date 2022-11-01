const VERSION_KEY = process.env.APP_ID + "_version";

export const getVersion = () => {
  return window.localStorage.getItem(VERSION_KEY);
};

export const saveVersion = (token) => {
  window.localStorage.setItem(VERSION_KEY, token);
};

export const destroyVersion = () => {
  window.localStorage.removeItem(VERSION_KEY);
};

export default { getVersion, saveVersion, destroyVersion };

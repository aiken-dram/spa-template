export const download = (response) => {
  //prepare file for downloading
  const blob = new Blob([response.data], { type: response.data.type });
  const url = window.URL.createObjectURL(blob);
  const link = document.createElement("a");
  link.href = url;
  const contentDisposition = response.headers["content-disposition"];
  let fileName = "unknown";
  if (contentDisposition) {
    const fileNameMatch = contentDisposition.match(/filename\*=UTF-8''(.+)/);
    if (fileNameMatch && fileNameMatch.length === 2)
      fileName = fileNameMatch[1];
  }
  link.setAttribute("download", decodeURIComponent(fileName));
  document.body.appendChild(link);
  link.click();
  link.remove();
  window.URL.revokeObjectURL(url);
};

export default { download };

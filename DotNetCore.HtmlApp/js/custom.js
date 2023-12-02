// success alertbox from sweetalert
function successMessage(message) {
  Swal.fire({
    title: "Success!",
    text: message, // give message as a parameter and customize at the other components
    icon: "success",
  });
}

// Warning alertBox from sweetalert
function warningMessage(message) {
  Swal.fire({
    title: "Warning!",
    text: message,
    icon: "warning",
  });
}

// uuid from stackoverflow (uiniversal unique id)
function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}

// delete alertBox from sweetalert
function confirmMessage(message) {
  return new Promise((resolve, reject) => {
    Swal.fire({
      title: "Confirm",
      text: message,
      icon: "warning",
      showCancelButton: true,
    }).then((result) => {
      // return result.isConfirmed;
      resolve(result.isConfirmed);
    });
  });
}

function notiflixConfirm(message) {
  return new Promise((resolve, result) => {
    Notiflix.Confirm.show(
      "Notiflix Confirm",
      "Do you agree with me?",
      "Yes",
      "No",
      function okCb() {
        resolve(true);
      },
      function cancelCb() {
        resolve(false);
      },
      {}
    );
  });
}

function clear() {
  $("#txtUserName").val("");
  $("#txtUserName").focus();
}

//Notiflix notification
function notiMessage() {
  Notiflix.Notify.success("Save Successful");
}

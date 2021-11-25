function renderJS(id, timeStamp) {
  window.requestAnimationFrame((t) => renderJS(id, t));
  window.scenes[id].instance.invokeMethodAsync('Run', timeStamp, window.scenes[id].canvas.width, window.scenes[id].canvas.height);
}

function onResize(id) {

  //TODO resize handle not fullscreen

  if (!window.scenes[id].canvas)
    return;

  window.scenes[id].canvas.width = window.innerWidth - window.scenes[id].holder.offsetLeft;

  window.scenes[id].canvas.height = window.innerHeight - window.scenes[id].holder.offsetTop;

}

function onClick(id, e) {
  console.log(window.scrollX);
  let holderLeft = window.scenes[id].holder.offsetLeft;
  let holderRight = window.scenes[id].holder.offsetTop;
  window.scenes[id].instance.invokeMethodAsync('OnClick', e.clientX - holderLeft + window.scrollX, e.clientY - holderRight + window.scrollY)
}

window.initRenderJS = (id, instance) => {

  if (window.scenes == null)
    window.scenes = {};

  let holder = document.getElementById(id);
  let canvase = holder.getElementsByTagName('canvas') || [];

  window.scenes[id] = {
    instance: instance,
    canvas: canvase.length ? canvase[0] : null,
    holder: holder
  };
  window.addEventListener("resize", () => onResize(id));

  //onResize();

  if (window.scenes[id].canvas) {
    window.scenes[id].canvas.onclick = (e) => {
      onClick(id, e);
    };
  }

  window.requestAnimationFrame((t) => renderJS(id, t));
};

window.createLinearGradient = (id, x0, y0, x1, y1, colorstops) => {

  if (window.scenes[id] == null)
    return null;

  let canvas = window.scenes[id].canvas;

  let ctx = canvas.getContext("2d");

  let grad = ctx.createLinearGradient(x0, y0, x1, y1);
  let len = colorstops.length - 1;
  colorstops.forEach(function (value, i) {
    grad.addColorStop(i / len, value);
  });
  return grad;
}
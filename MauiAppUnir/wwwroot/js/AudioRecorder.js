window.BlazorAudioRecorder = {
    mStream: null,
    mAudioChunks: [],
    mMediaRecorder: null,
    mCaller: null,

    Initialize: function (vCaller) {
        this.mCaller = vCaller;
    },

    StartRecord: async function () {
        this.mStream = await navigator.mediaDevices.getUserMedia({ audio: true });
        this.mMediaRecorder = new MediaRecorder(this.mStream);
        this.mAudioChunks = []; // Asegurarse de que mAudioChunks esté vacío antes de comenzar a grabar

        this.mMediaRecorder.addEventListener('dataavailable', vEvent => {
            this.mAudioChunks.push(vEvent.data);
        });

        this.mMediaRecorder.addEventListener('error', vError => {
            console.warn('Media recorder error: ' + vError);
        });

        this.mMediaRecorder.addEventListener('stop', () => {
            var pAudioBlob = new Blob(this.mAudioChunks, { type: "audio/mp3;" });
            var pAudioUrl = URL.createObjectURL(pAudioBlob);
            this.mCaller.invokeMethodAsync('OnAudioUrl', pAudioUrl);

            // Si quieres reproducir el audio grabado directamente sin usar un elemento HTML <audio>, descomenta las siguientes líneas:
            // var pAudio = new Audio(pAudioUrl);
            // pAudio.play();
        });

        this.mMediaRecorder.start();
    },

    PauseRecord: function () {
        this.mMediaRecorder.pause();
    },

    ResumeRecord: function () {
        this.mMediaRecorder.resume();
    },

    StopRecord: function () {
        this.mMediaRecorder.stop();
        this.mStream.getTracks().forEach(track => track.stop());
    },

    DownloadBlob: async function (vUrl) {
        const response = await fetch(vUrl);
        const audioBlob = await response.blob();
        const arrayBuffer = await audioBlob.arrayBuffer();
        const bytes = new Uint8Array(arrayBuffer);
        const base64String = btoa(String.fromCharCode(...bytes));
        return base64String;
    },
};
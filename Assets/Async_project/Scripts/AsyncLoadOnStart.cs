using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Async_project.Scripts
{
    public class AsyncLoadOnStart : MonoBehaviour
    {
        [SerializeField] private Transform _light;
        
        private string _filePath;

        private readonly ReactiveProperty<string> _url = new();
        private readonly ReactiveProperty<string> _config = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private void Awake()
        {
            _filePath = Application.dataPath + "/Async_project/File.txt";

            _url.SkipLatestValueOnSubscribe().Subscribe(url => {
                Debug.Log($"Url: {url}");
                SendRequest(url);
            }).AddTo(_compositeDisposable);

            _config.SkipLatestValueOnSubscribe().Subscribe(_ => {
                Debug.Log($"result: {_config.Value}");
            }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(_ => {
                _light.Rotate(180 * Time.deltaTime, 0, 0);
            }).AddTo(_compositeDisposable);
        }

        private async void Start()
        {
            Debug.Log("Start program");
            _url.Value = await Task.Run(LoadUrlFromConfigFile);
        }
        
        private string LoadUrlFromConfigFile()
        {
            Thread.Sleep(2000);
            string fileContent = File.ReadAllText(_filePath);
            string pattern = @"ConfigUrl\s*:\s*""([^""]+)""";
            Match match = Regex.Match(fileContent, pattern);
                
            return  match.Groups[1].Value;
        }

        private async void SendRequest(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            
            var result = await Task.Run(() => {
                Thread.Sleep(2000);
                WebResponse resp = webRequest.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                return sr.ReadToEnd();
            });
            
            _config.Value = result;
        }
    }
}

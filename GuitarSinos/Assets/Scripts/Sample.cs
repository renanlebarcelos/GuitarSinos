using UnityEngine;
using System.Collections.Generic;


public class Sample : MonoBehaviour
{
    AudioSource source;
    AudioSource source2;
    public GameObject[] cubes;
    public float delay;
    public int numActivators;

    public float SpectrumRefreshTime;
    public float limit = 0;
    private float lastUpdate = 0;
    private float midSpectrum = 0;
    private int baseCalc = 0;
    private float[] spectrum = new float[8192];
    public float scaleFactor = 10000;

    private int clipSamples;
    private int clipChannels;
    private float[] clipData;
    private float[] clipDataChannelOne;

    void Start()
    {
        source = GetComponent<AudioSource>();
        //source2 = GetComponent<AudioSource>();

        baseCalc = spectrum.Length / numActivators;
        //source2.PlayOneShot(source.clip, 0.0001f);
        source.Play();
    }

    void Update()
    {
        if (Time.time - lastUpdate > SpectrumRefreshTime)
        {
            source.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
            for (int i = 1536; i < spectrum.Length - 3584; i++)
            {
                midSpectrum += spectrum[i];

                if ((i+1) % baseCalc == 0) {
                    midSpectrum /= baseCalc;

                    if (midSpectrum > limit)
                    { 
                        GameObject cub = GameObject.Instantiate(cubes[(i / baseCalc)]);
                        cub.GetComponent<Note>().speed = 5;
                    }

                    midSpectrum = 0;
                }
            }
            lastUpdate = Time.time;
        }
    }

    void loadSong()
    {
        clipSamples = source.clip.samples;
        clipChannels = source.clip.channels;

        clipData = new float[clipSamples * clipChannels];

        source.clip.GetData(clipData, 0);
        source2.clip.GetData(clipData, 0);

        clipDataChannelOne = new float[clipSamples];

        int c = 0;
        for (int i = 0; i < clipData.Length; i += 2)
        {
            clipDataChannelOne[c] = clipData[i];
            ++c;
        }
    }
}
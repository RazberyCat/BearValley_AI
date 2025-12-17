using UnityEngine;

public class MapParallax : MonoBehaviour
{
    private float _length, _startpos;
    private GameObject _cam;
    public float parallaxValue;

    private float _moveSpeed = 10.0f;

    private void Start()
    {
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _cam = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        float temp = (_cam.transform.position.x * (1 - parallaxValue));
        float dist = (_cam.transform.position.x * parallaxValue);

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);

        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}

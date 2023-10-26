using System;
using UnityEngine;
namespace GameSoft.Tools.Extensions
{
	public static class TransformExtensions
	{
		public static void SetWidthDelta(this RectTransform rectTransform, float width)
		{
			Vector2 size = rectTransform.sizeDelta;
			size.x = width;
			rectTransform.sizeDelta = size;
		}

		public static void SetHeightDelta(this RectTransform rectTransform, float height)
		{
			Vector2 size = rectTransform.sizeDelta;
			size.y = height;
			rectTransform.sizeDelta = size;
		}

		public static void SetAnchorPositionX(this RectTransform rectTransform, float x)
		{
			Vector2 pos = rectTransform.anchoredPosition;
			pos.x = x;
			rectTransform.anchoredPosition = pos;
		}

		public static void SetAnchorPositionY(this RectTransform rectTransform, float y)
		{
			Vector2 pos = rectTransform.anchoredPosition;
			pos.y = y;
			rectTransform.anchoredPosition = pos;
		}

		public static void SetLocalPositionX(this Transform transform, float x)
		{
			Vector3 pos = transform.localPosition;
			pos.x = x;
			transform.localPosition = pos;
		}

		public static void SetLocalPositionY(this Transform transform, float y)
		{
			Vector3 pos = transform.localPosition;
			pos.y = y;
			transform.localPosition = pos;
		}

		public static void SetPositionX(this Transform transform, float x)
		{
			Vector3 pos = transform.position;
			pos.x = x;
			transform.position = pos;
		}

		public static void SetPositionY(this Transform transform, float y)
		{
			Vector3 pos = transform.position;
			pos.y = y;
			transform.position = pos;
		}

		public static void SetAnchorMaxX(this RectTransform rectTransform, float x)
		{
			Vector2 pos = rectTransform.anchorMax;
			pos.x = x;
			rectTransform.anchorMax = pos;
		}

		public static void SetAnchorMaxY(this RectTransform rectTransform, float y)
		{
			Vector2 pos = rectTransform.anchorMax;
			pos.y = y;
			rectTransform.anchorMax = pos;
		}

		public static void SetAnchorMinX(this RectTransform rectTransform, float x)
		{
			Vector2 pos = rectTransform.anchorMin;
			pos.x = x;
			rectTransform.anchorMin = pos;
		}

		public static void SetAnchorMinY(this RectTransform rectTransform, float y)
		{
			Vector2 pos = rectTransform.anchorMin;
			pos.y = y;
			rectTransform.anchorMin = pos;
		}

		public static void SetAnchorMinMaxX(this RectTransform rectTransform, float x)
		{
			Vector2 pos = rectTransform.anchorMin;
			pos.x = x;
			rectTransform.anchorMin = pos;
			pos = rectTransform.anchorMax;
			pos.x = x;
			rectTransform.anchorMax = pos;
		}

		public static void SetAnchorMinMaxY(this RectTransform rectTransform, float y)
		{
			Vector2 pos = rectTransform.anchorMin;
			pos.y = y;
			rectTransform.anchorMin = pos;
			pos = rectTransform.anchorMax;
			pos.y = y;
			rectTransform.anchorMax = pos;
		}

		public static void SetScale2D(this Transform transform, float scale)
		{
			transform.SetScale2D(scale, scale);
		}

		public static void SetScale2D(this Transform transform, Vector2 scale)
		{
			transform.SetScale2D(scale.x, scale.y);
		}

		public static void SetScale2D(this Transform transform, float x, float y)
		{
			transform.SetScale(x, y, 1);
		}

		public static void SetScale(this Transform transform, float scale)
		{
			transform.SetScale(scale, scale, scale);
		}

		public static void SetScale(this Transform transform, float x, float y, float z)
		{
			transform.SetScale(new Vector3(x, y, z));
		}

		public static void SetScale(this Transform transform, Vector3 scale)
		{
			transform.localScale = scale;
		}

		public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
		{
			if (rectTransform == null) return;

			Vector2 size = rectTransform.rect.size;
			Vector2 deltaPivot = rectTransform.pivot - pivot;
			Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
			rectTransform.pivot = pivot;
			rectTransform.localPosition -= deltaPosition;
		}

		public static Transform FindPath(this Transform transform, string path)
		{
			var parts = path.Split('/');
			var result = transform;
			foreach (string t in parts)
			{
				if (string.IsNullOrEmpty(t))
				{
					continue;
				}

				var found = result.Find(t);
				if (found == null)
				{
					throw new NullReferenceException($"Cant find part \"{t}\" of path \"{path}\"");
				}

				result = found;
			}

			return result;
		}
	}
}
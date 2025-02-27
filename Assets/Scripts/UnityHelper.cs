﻿/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： Unity 帮助脚本
 *    Description: 
 *           功能： 提供程序用户一些常用的功能方法实现，方便程序员快速开发。
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindFunc
{
	public class UnityHelper : MonoBehaviour {
        
        /// <summary>
        /// 查找子节点对象
        /// 内部使用“递归算法”
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="chiildName">查找的子对象名称</param>
        /// <returns></returns>
	    public static Transform FindTheChildNode(GameObject goParent,string chiildName)
        {
            return FindTheChildNode(goParent.transform, chiildName);
        }
        /// <summary>
          /// 查找子节点对象
          /// 内部使用“递归算法”
          /// </summary>
          /// <param name="goParent">父对象</param>
          /// <param name="chiildName">查找的子对象名称</param>
          /// <returns></returns>
        public static Transform FindTheChildNode(Transform tfParent, string chiildName)
        {
            Transform searchTrans = null;                   //查找结果

            searchTrans = tfParent.Find(chiildName);
            if (searchTrans == null)
            {
                foreach (Transform trans in tfParent)
                {
                    searchTrans = FindTheChildNode(trans, chiildName);
                    if (searchTrans != null)
                    {
                        return searchTrans;

                    }
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
	    public static T GetTheChildNodeComponetScripts<T>(GameObject goParent, string childName) where T : Component
        {
            return GetTheChildNodeComponetScripts<T>(goParent.transform, childName);
        }
        /// <summary>
        /// 获取子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tfParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
	    public static T GetTheChildNodeComponetScripts<T>(Transform tfParent, string childName) where T : Component
        {
            Transform searchTranformNode = null;            //查找特定子节点

            searchTranformNode = FindTheChildNode(tfParent, childName);
            if (searchTranformNode != null)
            {
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取父节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheParentNodeComponetScripts<T>(Transform childTransform) where T : Component
        {
            return childTransform.GetComponentInParent<T>();
        }
        /// <summary>
        /// 给子节点添加脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
	    public static T AddChildNodeCompnent<T>(GameObject goParent,string childName) where T:Component
        {
            Transform searchTranform = null;                //查找特定节点结果

            //查找特定子节点
            searchTranform = FindTheChildNode(goParent, childName);
            //如果查找成功，则考虑如果已经有相同的脚本了，则先删除，否则直接添加。
            if (searchTranform!=null)
            {
                //如果已经有相同的脚本了，则先删除
                T[] componentScriptsArray=searchTranform.GetComponents<T>();
                for (int i = 0; i < componentScriptsArray.Length; i++)
                {
                    if (componentScriptsArray[i]!=null)
                    {
                        Destroy(componentScriptsArray[i]);
                    }
                }
                return searchTranform.gameObject.AddComponent<T>();
            }
            else
            {
                return null;
            }
            //如果查找不成功，返回Null.
        }

        /// <summary>
        /// 给子节点添加父对象
        /// </summary>
        /// <param name="parents">父对象的方位</param>
        /// <param name="child">子对象的方法</param>
	    public static void AddChildNodeToParentNode(Transform parents, Transform child)
	    {
            child.SetParent(parents,false);
	        child.localPosition = Vector3.zero;
	        child.localScale = Vector3.one;
	        child.localEulerAngles = Vector3.zero;
	    }
	}
}